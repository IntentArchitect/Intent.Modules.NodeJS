import { TptFkDerivedClass } from './tpt-fk-derived-class.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tpt_fk_base_class_associated')
export class TptFkBaseClassAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  fkDerivedClassCompositeKeyA: string;

  @Column()
  fkDerivedClassCompositeKeyB: string;

  @ManyToOne(() => TptFkDerivedClass, { cascade: ['insert', 'update'], nullable: false })
  fkDerivedClass: TptFkDerivedClass;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}