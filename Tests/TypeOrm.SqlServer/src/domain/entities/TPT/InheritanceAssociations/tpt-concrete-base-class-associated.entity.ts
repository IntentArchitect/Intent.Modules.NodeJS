import { TptConcreteBaseClass } from './tpt-concrete-base-class.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tpt_concrete_base_class_associated')
export class TptConcreteBaseClassAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  concreteBaseClassId: string;

  @ManyToOne(() => TptConcreteBaseClass, { cascade: ['insert', 'update'], nullable: false })
  concreteBaseClass: TptConcreteBaseClass;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}