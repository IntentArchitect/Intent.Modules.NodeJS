import { TpcConcreteBaseClass } from './tpc-concrete-base-class.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tpc_concrete_base_class_associated')
export class TpcConcreteBaseClassAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  concreteBaseClassId: string;

  @ManyToOne(() => TpcConcreteBaseClass, { cascade: ['insert', 'update'], nullable: false })
  concreteBaseClass: TpcConcreteBaseClass;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}